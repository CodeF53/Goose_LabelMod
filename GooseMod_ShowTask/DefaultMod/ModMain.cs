using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 1. Added the "GooseModdingAPI" project as a reference.
// 2. Compile this.
// 3. Create a folder with this DLL in the root, and *no GooseModdingAPI DLL*
using GooseShared;
using SamEngine;
using static GooseShared.API;
using static GooseShared.API.TaskDatabaseQueryFunctions;

namespace DefaultMod
{
    public class ModEntryPoint : IMod
    {
        // Gets called automatically, passes in a class that contains pointers to
        // useful functions we need to interface with the goose.
        public SolidBrush brushBlack, brushWhite;
        public Pen pen;
        String[] tasks;
        void IMod.Init()
        {
            tasks = API.TaskDatabase.getAllLoadedTaskIDs();
            brushBlack = new SolidBrush(System.Drawing.Color.Black);
            brushWhite = new SolidBrush(System.Drawing.Color.White);
            pen = new Pen(Brushes.White);
            // Subscribe to whatever events we want
            InjectionPoints.PostRenderEvent += PostRender;
        }

        public void PostRender(GooseEntity g, Graphics graph)
        {
            String taskNow = tasks[g.currentTask];
            PointF aaa = new PointF(g.position.x + 10, g.position.y + 10);
            Point bbb = new Point((int)(g.position.x +10), (int)g.position.y + 10);
            graph.FillRectangle(brushWhite, new Rectangle(bbb, new Size(12*10, 14)));
            graph.DrawString(taskNow, SystemFonts.DefaultFont, brushBlack, aaa);
        }
    }
}
