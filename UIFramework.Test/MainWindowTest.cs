﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIFramework;
using Veldrid.Sdl2;
using ImGuiNET;

namespace UIFramework
{
    public class MainWindowTest : MainWindow 
    {
        DockSpaceWindow DockSpace = new DockSpaceWindow("DockSpace");

        public MainWindowTest() 
        {
            this.MenuItems.Add(new MenuItem("Test", Add));

            var window = new TestWindow(DockSpace);
            window.DockDirection = ImGuiDir.Left;
            window.SplitRatio = 0.25f;
            DockSpace.DockedWindows.Add(window);

            DockSpace.DockedWindows.Add(new DockWindow(DockSpace,"Document")
            {
                DockDirection = ImGuiDir.None,
            });
            DockSpace.DockedWindows.Add(new DockWindow(DockSpace, "Properties")
            {
                DockDirection = ImGuiDir.Right,
                SplitRatio = 0.25f,
            });
            DockSpace.DockedWindows.Add(new DockWindow(DockSpace, "Console")
            {
                DockDirection = ImGuiDir.Down,
                SplitRatio = 0.25f,
            });
        }

        private void Add()
        {
            ImguiFileDialog dlg = new ImguiFileDialog();
            dlg.AddFilter(".png", "Portable Network Grahpics");
            dlg.SaveDialog = false;
            dlg.MultiSelect = true;
            if (dlg.ShowDialog())
            {

            }
        }

        public override void Render()
        {
            base.Render();

            var contentSize = ImGui.GetWindowSize();

            unsafe
            {
                //Constrain the docked windows within a workspace using window classes
                ImGui.SetNextWindowClass(window_class);
                //Set the window size on load
                ImGui.SetNextWindowSize(contentSize, ImGuiCond.Once);
            }
            DockSpace.Show();
        }
    }
}
