using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace CoopeguanacasteTiendaWeb.Paginas
{
    internal class PaginaBase : DriverHelper
    {
        protected String urlCoopeguanaste = "https://tienda.coopeguanacaste.com/";
        private WebDriverWait espera;

        public PaginaBase(IWebDriver driver)
        {
            this.driver = driver;
            espera = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            driver.Manage().Window.Maximize();
        }
        public void IrALaPagina(string ext = null)
        {
            driver.Navigate().GoToUrl(urlCoopeguanaste+ext);
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

        public String ObtenerTituloPagina()
        {
            return driver.Title;
        }

        public void LimpiarPruebas()
        {
             driver.Close();
             driver.Dispose();
        }
    }
}
