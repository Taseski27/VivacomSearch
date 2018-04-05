using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using API.Implementations;

namespace VivacomSearch.Hubs
{
    public class SearchHub : Hub
    {
        public void Search(string keyword, string folderPath, string folderText)
        {
            FileOperations operations = new FileOperations();
            var result = operations.GetMatchedObjects(folderPath, folderText, keyword);
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(result);
        }
    }
}