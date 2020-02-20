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

        //set before goose modifications
        float MouseX;
        float MouseY;
        void IMod.Init()
        {
            tasks = API.TaskDatabase.getAllLoadedTaskIDs();
            brushBlack = new SolidBrush(System.Drawing.Color.Black);
            brushWhite = new SolidBrush(System.Drawing.Color.White);
            pen = new Pen(Brushes.White);
            // Subscribe to whatever events we want
            InjectionPoints.PostRenderEvent += PostRender;
            InjectionPoints.PreTickEvent += PreTick;
        }

        public void PreTick(GooseEntity g)
        {
            MouseX = Input.mouseX;
            MouseY = Input.mouseY;
        }

        public void PostRender(GooseEntity g, Graphics graph)
        {
            drawTask(g, graph);
            drawXYpos(g, graph);
            drawCursorDistance(g, graph);
        }

        public void InfoLine(GooseEntity g, Graphics graph, String txt, int lnNum)
        {
            Size txtLen = graph.MeasureString(txt, SystemFonts.DefaultFont).ToSize();
            PointF txtPos = new PointF(g.position.x + 25, g.position.y + 25 + 14*lnNum);

            graph.FillRectangle(brushWhite, new Rectangle(Point.Round(txtPos), txtLen));
            graph.DrawString(txt, SystemFonts.DefaultFont, brushBlack, txtPos);
        }

        public void drawCursorDistance(GooseEntity g, Graphics graph)
        {
            Vector2 gPos = g.position;
            float mXDist = gPos.x - MouseX;
            float mYDist = gPos.y - MouseY;
            float mDist = (float)Math.Sqrt(Math.Pow(mXDist,2)+ Math.Pow(mYDist, 2));
            
            String str = "dist to Mouse: " + Math.Round(mDist, 2) + "";

            InfoLine(g, graph, str, 2);
        }
        public void drawXYpos(GooseEntity g, Graphics graph)
        {
            Vector2 gPos = g.position;
            String gp = "xy: (" + Math.Round(gPos.x) + ", " + Math.Round(gPos.y) + ")";
            InfoLine(g, graph, gp, 1);
        }

        public void drawTask(GooseEntity g, Graphics graph)
        {
            InfoLine(g, graph, tasks[g.currentTask], 0);
        }
    }
}
