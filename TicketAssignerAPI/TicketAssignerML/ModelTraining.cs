using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Tensorflow;
using Tensorflow.NumPy;
using static Tensorflow.Binding;
using static Tensorflow.KerasApi;
using TicketAssignerAPI.Model;
namespace TicketAssignerAPI.TicketAssignerML
{
    public class ModelTraining
    {
        public static void TrainModel(string dataPath, string modelPath)
        {
            var data = LoadData(dataPath);
            var (features, labels) = PreprocessData(data);

            var model = keras.Sequential();
            model.add(keras.layers.Dense(64, activation: "relu", input_shape: new Shape((int)5)));
            model.add(keras.layers.Dense(32, activation: "relu"));
            model.add(keras.layers.Dense(1, activation: "sigmoid"));

            model.compile(optimizer: keras.optimizers.Adam(), loss: keras.losses.BinaryCrossentropy(), metrics: new[] { "accuracy" });
            model.fit(features, labels, epochs: 50, batch_size: 32, validation_split: 0.2f);
            model.save(modelPath);
        }

        private static List<Ticket> LoadData(string filePath)
        {
            var data = new List<Ticket>();
            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines.Skip(1))
            {
                var fields = line.Split(',');
                data.Add(new Ticket
                {
                    Category = fields[1],
                    TotalEstimate = float.Parse(fields[5], CultureInfo.InvariantCulture),
                    LoggedEstimate = float.Parse(fields[6], CultureInfo.InvariantCulture),
                    Priority = fields[7],
                    TicketCount = float.Parse(fields[9], CultureInfo.InvariantCulture),
                    IsAssigned = fields[10] == "1"
                });
            }
            return data;
        }

        private static (NDArray features, NDArray labels) PreprocessData(List<Ticket> data)
        {
            var features = new List<float[]>();
            var labels = new List<float>();

            foreach (var ticket in data)
            {
                features.Add(new float[]
                {
                    ConvertCategoryToFloat(ticket.Category),
                    ticket.TotalEstimate,
                    ticket.LoggedEstimate,
                    ConvertPriorityToFloat(ticket.Priority),
                    ticket.TicketCount
                });
                labels.Add(ticket.IsAssigned ? 1.0f : 0.0f);
            }

            return (np.array(features.ToArray()), np.array(labels.ToArray()));
        }

        private static float ConvertCategoryToFloat(string category) => category == "bug" ? 0.0f : 1.0f;
        private static float ConvertPriorityToFloat(string priority) => priority switch
        {
            "major" => 0.0f,
            "critical" => 1.0f,
            "minor" => 2.0f,
            "trivial" => 3.0f,
            _ => 0.0f
        };
    }
}
