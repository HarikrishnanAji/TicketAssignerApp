using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Text;

public class JiraTicket
{
    public string TicketId { get; set; }
    public string Category { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime CompletedDate { get; set; }
    public double TotalEstimate { get; set; }
    public double LoggedEstimate { get; set; }
    public string Priority { get; set; }
    public int TicketCount { get; set; }
    public string AssignedTo { get; set; }
    public int IsAssigned { get; set; }
}

public class Program
{
    public static void Main()
    {
        var random = new Random();
        int nTickets = 20;
        var categories = new[] { "bug", "vulnerability" };
        var priorities = new[] { "critical", "major", "minor", "trivial" };
        var startDateRange = new DateTime(2023, 1, 1);
        var endDateRange = new DateTime(2023, 12, 31);
        var tickets = new List<JiraTicket>();
        var teamMembers = new[] { "Dany", "Jon", "Aegon", "Arya", "Jorah","Ned","Robert","Jaime","Missandei"};


        for (int i = 0; i < nTickets; i++)
        {
            var ticketId = $"M-{i + 1:1000}";
            var category = categories[random.Next(categories.Length)];
            var createdDate = RandomDate(random, startDateRange, endDateRange);
            var startDate = createdDate.AddDays(random.Next(0, 11));
            int completionDays = GetCompletionDays(random, category);
            var completedDate = startDate.AddDays(completionDays);
            var totalEstimate = Math.Round(RandomDouble(random, 1, 7));
            var loggedEstimate = Math.Round(RandomDouble(random, 1, 5));
            var priority = priorities[random.Next(priorities.Length)];
            var assignedTo = teamMembers[random.Next(teamMembers.Length)];

            tickets.Add(new JiraTicket
            {
                TicketId = ticketId,
                Category = category,
                CreatedDate = createdDate,
                StartDate = startDate,
                CompletedDate = completedDate,
                TotalEstimate = totalEstimate,
                LoggedEstimate = loggedEstimate,
                Priority = priority,
                AssignedTo = assignedTo
            });
        }

        // Write the tickets to a CSV file
        var ticketCounts = tickets
           .GroupBy(t => t.AssignedTo)
           .Select(g => new { Assignee = g.Key, TicketCount = g.Count(), TotalEstimate = g.Sum(t => t.TotalEstimate) })
           .ToDictionary(x => x.Assignee, x => new { x.TicketCount, x.TotalEstimate });

            var csvPath = "../../../jira_tickets.csv";
        using (var writer = new StreamWriter(csvPath, false, Encoding.UTF8))
        {
            writer.WriteLine("TicketId,Category,CreatedDate,StartDate,CompletedDate,TotalEstimate,LoggedEstimate,Priority,AssignedTo,TicketCount","IsAssigned");
            foreach (var ticket in tickets)
            {
                var counts = ticketCounts[ticket.AssignedTo];
                ticket.TicketCount = counts.TicketCount;
                ticket.IsAssigned = (counts.TicketCount < 2 || counts.TotalEstimate < 3) ? 1 : 0;
                writer.WriteLine($"{ticket.TicketId},{ticket.Category},{ticket.CreatedDate:yyyy-MM-dd},{ticket.StartDate:yyyy-MM-dd},{ticket.CompletedDate:yyyy-MM-dd},{ticket.TotalEstimate:F2},{ticket.LoggedEstimate:F2},{ticket.Priority},{ticket.AssignedTo},{ticket.TicketCount},{ticket.IsAssigned}");
            }
        }

        Console.WriteLine($"CSV file generated at: {Path.GetFullPath(csvPath)}");
    }

    private static DateTime RandomDate(Random random, DateTime start, DateTime end)
    {
        var range = (end - start).Days;
        return start.AddDays(random.Next(range));
    }
    private static double RandomDouble(Random random, double minValue, double maxValue)
    {
        return random.NextDouble() * (maxValue - minValue) + minValue;
    }

    private static int GetCompletionDays(Random random, string category)
    {
        if (category == "bug")
        {
            // 80% chance of taking up to 7 days, 20% chance of taking more
            return random.NextDouble() < 0.9 ? random.Next(1, 4) : random.Next(4, 7);
        }
        else // "vulnerability"
        {
            // 80% chance of taking at least 7 days, 20% chance of taking more
            return random.NextDouble() < 0.9 ? random.Next(1, 3) : random.Next(3, 5);
        }
    }
}
