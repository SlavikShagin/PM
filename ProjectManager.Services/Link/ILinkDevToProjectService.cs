using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Services.Link
{
    public interface ILinkDevToProjectService
    {
        Task Link(int developerId, int projectId);
        Task UnLink(int developerId, int projectId);
    }
}
