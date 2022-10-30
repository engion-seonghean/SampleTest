using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FuncEvent;
namespace PmacIO
{
    //public class FolderInfo
    //{
    //    public string sName { get; set; }
    //    public string sPath { get; set; }
    //}

    public partial class ucRecipeShow : UserControl
    {
        class FolderInfo
        {
            public string sName { get; set; }
            public string sPath { get; set; }
        }
        List<FolderInfo> recipeList = new List<FolderInfo>();
        string CurRecipe = string.Empty;
      //  Log log = Vars.log;
        public ucRecipeShow()
        {
            InitializeComponent();
        }
        string RecipeFolder = @"C:\Temp\Recipe";

        private void ucRecipeShow_Load(object sender, EventArgs e)
        {
            LoadRecipeName();
        }
        private void listView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            //using (Brush hBr = new SolidBrush(Color.GreenYellow))
            //{
            //    e.Graphics.FillRectangle(hBr, e.Bounds);
            //    e.DrawText();
            //}
        }
        public void LoadRecipeName()
        {
            recipeList.Clear();
            listView1.Items.Clear();
            if (Directory.Exists(RecipeFolder))
            {
                DirectoryInfo di = new DirectoryInfo(RecipeFolder);
                foreach (var item in di.GetDirectories())
                {
                    recipeList.Add(new FolderInfo() { sName = item.Name, sPath = item.FullName });
                    listView1.Items.Add(new ListViewItem(item.Name));
                }
                foreach (var file in di.GetFiles())
                {
                    //string extension = Path.GetExtension(file.FullName);
                    //if (extension == ".Json")
                    //{
                    //    fileName = Path.GetFileNameWithoutExtension(file.FullName);
                    //    listView1.Items.Add(new ListViewItem(fileName));

                    //    recipeList.Add(file.FullName);
                    //}
                }
            }
        }

        private void btnLoadRecipe_Click(object sender, EventArgs e)
        {
            try
            {
                CurRecipe = listView1.SelectedItems[0].Name;

                if (MessageBox.Show($"선택하신 Recipe[{CurRecipe}]를 적용하시겠습니까?", "Recipe 적용", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                LoadRecipeData(CurRecipe);
            }
            catch (Exception ex)
            {
                //log.AddLogMessage(LogType.Error, 0, ex);
            }
        }

        private void LoadRecipeData(string v)
        {
            lbCurRecipe.Text = CurRecipe= v;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string selectRecipe = listView1.SelectedItems[0].Name;

            if (MessageBox.Show($"선택하신 Recipe[{selectRecipe}]를 삭제하시겠습니까?", "Recipe 적용", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            FolderInfo FullDel = recipeList.Find(d => d.sName == selectRecipe);
            DirectoryInfo dir = new DirectoryInfo(FullDel.sPath);

            System.IO.FileInfo[] files = dir.GetFiles("*.*", SearchOption.AllDirectories);

            foreach (System.IO.FileInfo file in files)
                file.Attributes = FileAttributes.Normal;

            Directory.Delete(FullDel.sPath, true);

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                int SelectRow = listView1.SelectedItems[0].Index;

                string a = listView1.Items[SelectRow].SubItems[0].Text;
                LoadRecipeData(a);
                CurRecipe = a;
            }

        }
    }
}
