using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zapoctak_ProgramovaniII_ls2014
{
    class View
    {
        Model model;
        public View(Model m) {
            this.model = m;       
        }
        public void Action() {

            throw new NotImplementedException();
        }
        private void DestructDirty() {
            foreach (var item in model.dirtyDestruct)
            {
                item.Dispose();
            }               
        }
        private void NewDirty() { 
            
        
        }
    }
}
