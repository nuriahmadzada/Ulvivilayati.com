using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Portfolio.Models;

namespace Portfolio.ViewModel
{
    public class ViewDatas
    {
        public Info Info { get; set; }

        public About About { get; set; }

        public List<Service> Services { get; set; }

        public List<Fact> Facts { get; set; }

        public List<Education> Educations { get; set; }

        public List<Experience> Experiences { get; set; }

        public List<Skill> Skills { get; set; }

        public List<Category> Categories { get; set; }

        public List<Project> Projects { get; set; }

        public List<Photo> Photos { get; set; }

        public List<TechnologyProject> TechnologyProjects { get; set; }
    }
}