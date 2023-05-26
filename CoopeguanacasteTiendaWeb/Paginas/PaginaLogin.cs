using OpenQA.Selenium;
using System.Threading;

namespace CoopeguanacasteTiendaWeb.Paginas
{
    internal class PaginaLogin 
    {
        private IWebDriver driver;
        private PaginaBase paginaBase;
        public PaginaLogin(IWebDriver driver)
        {
            this.driver = driver;
            paginaBase = new PaginaBase(driver);
        }
        IWebElement btnMenuPersonal => driver.FindElement(By.XPath("//div[@id='header-links-opener']/span"));

        IWebElement btnIniciarSesion => driver.FindElement(By.XPath("//a[contains(text(),'Iniciar sesión')]"));

        IWebElement lblRecordarme => driver.FindElement(By.XPath("//label[contains(.,'Recordarme!')]"));

        IWebElement btnCerrarSesion => driver.FindElement(By.XPath("//a[contains(text(),'Cerrar sesión')]"));

        IWebElement txtNombreUsuario => driver.FindElement(By.Id("Username"));

        IWebElement txtContrasenna => driver.FindElement(By.Id("Password"));

        IWebElement btnLogin => driver.FindElement(By.CssSelector(".login-button"));

        public void IngresarEmailyContrasenna(string userName, string pass)
        {
            btnMenuPersonal.Click();
            btnIniciarSesion.Click();
            Thread.Sleep(1);
            txtNombreUsuario.Click();
            txtNombreUsuario.Clear();
            txtNombreUsuario.SendKeys(userName);
            Thread.Sleep(2);
            txtContrasenna.Click();
            txtContrasenna.Clear();
            txtContrasenna.SendKeys(pass);
            lblRecordarme.Click();
            btnLogin.Click();
            Thread.Sleep(2);
            paginaBase.EsperaTituloContenga("Tienda Coopeguanacaste");
        }
        public void CerrarSesion()
        {
            btnMenuPersonal.Click();
            Thread.Sleep(2);
            btnCerrarSesion.Click();
            Thread.Sleep(2);
        }
    }
}
