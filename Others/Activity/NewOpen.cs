﻿using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Example.Workflow
{
    public class NewOpen : NativeActivity<ILog>
    {
        [RequiredArgument]
        public InArgument<string> BookmarkName { get; set; }

        protected override void Execute(NativeActivityContext context)
        {

            // Create a Bookmark and wait for it to be resumed.  
            context.CreateBookmark(BookmarkName.Get(context),
                new BookmarkCallback(OnResumeBookmark));
        }

        // NativeActivity derived activities that do asynchronous operations by calling   
        // one of the CreateBookmark overloads defined on System.Activities.NativeActivityContext   
        // must override the CanInduceIdle property and return true.  
        protected override bool CanInduceIdle
        {
            get { return true; }
        }

        public void OnResumeBookmark(NativeActivityContext context, Bookmark bookmark, object obj)
        {
            // When the Bookmark is resumed, assign its value to  
            // the Result argument.  
            Result.Set(context, (ILog)obj);
        }

    }
}
