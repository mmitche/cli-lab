using System.Collections.Concurrent;
using Microsoft.Build.Framework;

namespace Microsoft.Build.Logging.Query.Component
{
    public class Build : Component
    {
        public ConcurrentDictionary<int, Project> Projects { get; }
        public ConcurrentBag<Project> OrderedProjects { get; }
        public override Component Parent => null;

        public Build() : base()
        {
            Projects = new ConcurrentDictionary<int, Project>();
            OrderedProjects = new ConcurrentBag<Project>();
        }

        public Project GetOrAddProject(int id, ProjectStartedEventArgs args)
        {
            var project = new Project(
                id,
                args.ProjectFile,
                args.TargetNames,
                args.Items,
                args.Properties,
                args.GlobalProperties,
                this);

            if (Projects.TryAdd(id, project))
            {
                OrderedProjects.Add(project);
                return project;
            }

            return Projects[id];
        }
    }
}