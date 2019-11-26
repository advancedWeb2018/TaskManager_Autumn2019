using MakeIt.DAL.EF;
using System.Collections.Generic;
using System.Data.Entity;

namespace MakeIt.DAL.ModelInitializer
{
    class ContextInitializer : CreateDatabaseIfNotExists<MakeItContext>
    {
        protected override void Seed(MakeItContext context)
        {
            var listColors = new List<Color>
            {
                new Color {Name = "aqua", Code="#00ffff"},
                new Color {Name = "black", Code="#000000"},
                new Color {Name = "blue", Code="#0000ff"},
                new Color {Name = "fuchsia", Code="#ff00ff"},
                new Color {Name = "gray", Code="#808080"},
                new Color {Name = "green", Code="#008000"},
                new Color {Name = "lime", Code="#00ff00"},
                new Color {Name = "maroon", Code="#800000"},
                new Color {Name = "navy", Code="#000080"},
                new Color {Name = "olive", Code="#808000"},
                new Color {Name = "purple", Code="#800080"},
                new Color {Name = "red", Code="#ff0000"},
                new Color {Name = "silver", Code="#c0c0c0"},
                new Color {Name = "teal", Code="#008080"},
                new Color {Name = "white", Code="#ffffff"},
                new Color {Name = "yellow", Code="#ffff00"}
            };
            context.Colors.AddRange(listColors);

            var listPriorities = new List<Priority>
            {
                new Priority {Name = "Low"},
                new Priority {Name = "Middle"},
                new Priority {Name = "High"}
            };
            context.Priorities.AddRange(listPriorities);

            var listStatuses = new List<Status>
            {
                new Status {Name = "Open"},
                new Status {Name = "Asigned"},
                new Status {Name = "In work"},
                new Status {Name = "Done"},
                new Status {Name = "Closed"},
                new Status {Name = "Canceled"}
            };
            context.Statuses.AddRange(listStatuses);
            context.SaveChanges();
        }
    }
}
