
using static Tensorflow.Binding;
using static Tensorflow.KerasApi;
using Tensorflow.NumPy;
using Tensorflow;
using TicketAssignerAPI.Model;

namespace TicketAssignerAPI.TicketAssignerML
{
    public class ModelPrediction
    {
        public static bool Predict(string modelPath, Ticket ticket)
        {
            var model = keras.models.load_model(modelPath);

            var features = new float[]
            {
                ConvertCategoryToFloat(ticket.Category),
                ticket.TotalEstimate,
                ticket.LoggedEstimate,
                ConvertPriorityToFloat(ticket.Priority),
                ticket.TicketCount
            };

            var input = np.array(new float[][] { features });
            var prediction = model.predict(input);
            var predictionArray = prediction.numpy() as NDArray;
            var predictionValue = (float)predictionArray[0][0];

            return predictionValue > 0.5f;
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
