using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopeguanacasteTiendaWeb
{
    internal class PaginaBase
    {
        private IWebDriver driver;
        public PaginaBase(IWebDriver _driver) {
            driver = _driver;
        }
    }
}
