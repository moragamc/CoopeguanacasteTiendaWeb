using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace CoopeguanacasteTiendaWeb.Paginas
{
    internal class PaginaBase
    {
        private IWebDriver driver;
        protected String urlCoopeguanaste = "https://tienda.coopeguanacaste.com/";
        private WebDriverWait espera;

        public PaginaBase(IWebDriver driver)
        {
            this.driver = driver;
            espera = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            driver.Manage().Window.Maximize();
        }
        public void goToPage(string ext = null)
        {
            driver.Navigate().GoToUrl(urlCoopeguanaste+ext);
        }

        public void BuscarElemento(By elemento)
        {
            driver.FindElement(elemento);
        }

        public void BuscarElementos(By elemento)
        {
            driver.FindElements(elemento);
        }

        public void EsperaTituloContenga(String texto) 
        {
            espera.Until(ExpectedConditions.TitleContains(texto));
        }

        public void EsperaASerCliclado(By elemento)
        {
            espera.Until(ExpectedConditions.ElementToBeClickable(elemento));
        }

        public void EsperaElementoVisible(By elemento)
        {
            espera.Until(ExpectedConditions.ElementIsVisible(elemento));
        }

        public String getPageTitle()
        {
            return driver.Title;
        }
    }
}
