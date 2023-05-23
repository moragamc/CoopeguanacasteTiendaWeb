using OpenQA.Selenium;

namespace CoopeguanacasteTiendaWeb.Paginas
{
    internal class PaginaLogin : DriverHelper
    {
        private IWebDriver Driver;

        public PaginaLogin(IWebDriver driver)
        {
            Driver = driver;
        }
        IWebElement txtNombreUsuario => Driver.FindElement(By.Id("Username"));

        IWebElement txtContrasenna => Driver.FindElement(By.Id("Password"));

        IWebElement btnLogin => Driver.FindElement(By.CssSelector(".login-button"));

        public void ingresarEmailyPassword(string userName, string pass)
        {
            txtNombreUsuario.Clear();
            txtNombreUsuario.SendKeys(userName);
            txtContrasenna.Clear();
            txtContrasenna.SendKeys(pass);

        }

        public void ClickLogin()
        {
            btnLogin.Click();
        }
    }
}
