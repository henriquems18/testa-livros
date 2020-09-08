using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace testa_livros
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AbrirNavegadorPesquisar()
        {
            // VARIAVEIS
            string strISBN = null;
            string strAutor = null;
            string strAutorMega = null;
            string strAutorSara = null;
            bool isISBN = false;
            bool isAutor = false;

            /* --------------------------------SUBMARINO-------------------------------- */

            // ENTRANDO NO SITE SUBMARINO
            IWebDriver chrome = new ChromeDriver();
            chrome.Navigate().GoToUrl("https://www.submarino.com.br/categoria/livros?chave=prf_hm_mn_bn_0_2_livros");

            // CLICANDO NO PRIMEIRO LIVRO DO SITE
            IWebElement eleSubLink = chrome.FindElement(By.CssSelector("a[class='card-product-url']"));
            eleSubLink.Click();

            //  BUSCANDO DADOS DO LIVRO
            IWebElement eleSub = chrome.FindElement(By.CssSelector("table[class='TableUI-v0rmpz-0 cWCwoD']"));

            // PEGANDO O CÓDIGO DO ISBN E AUTOR
            var colEleSub = eleSub.FindElements(By.CssSelector("span[class='TextUI-sc-12tokcy-0 CIZtP']"));
            foreach (var span in colEleSub)
            {
                if (isISBN == true)
                {
                    strISBN = span.Text;
                    isISBN = false;
                }

                if (isAutor == true)
                {
                    strAutor = span.Text;
                    break;
                }

                if (span.Text.Trim() == "ISBN-13")
                {
                    isISBN = true;
                }

                if (span.Text.Trim() == "Autor")
                {
                    isAutor = true;
                }
            }

            /* --------------------------------SUBMARINO-------------------------------- */

            /* ------------------------------MEGA LEITORES------------------------------ */
            
            // ENTRANDO NO SITE MEGA LEITORES
            chrome.Navigate().GoToUrl("https://www.megaleitores.com.br/");

            // LOCALIZANDO O LIVRO           
            chrome.FindElement(By.Name("livro")).SendKeys(strISBN);
            chrome.FindElement(By.Name("livro")).SendKeys(Keys.Enter);

            // CLICANDO NO LIVRO PESQUISADO
            IWebElement eleMegaLink = chrome.FindElement(By.CssSelector("h2[class='single-product-name']"));
            eleMegaLink.Click();

            // BUSCANDO AUTOR E ISBN
            IWebElement eleMega = chrome.FindElement(By.Id("reviews"));

            // PEGANDO DADOS DO AUTOR E ISBN

            var colMega = eleMega.FindElements(By.CssSelector("p[class='TextUI-sc-12tokcy-0 bLZSPZ']"));

            isAutor = false;
            foreach (var p in colMega)
            {
                if (isAutor == true)
                   {
                      strAutorMega = p.Text;
                      break;
                   }

                      if (p.Text.Trim() == "Autor")
                         {
                             isAutor = true;
                          }
            }

            // VERIFICANDO SE AUTOR E DIFERENTE
                if (strAutor != strAutorMega)
                   {
                       Assert.Fail("O autor do livro da Mega Leitores é diferente do autor do livro no submarino");
                   }
         
            /* ------------------------------MEGA LEITORES------------------------------ */

            /* ---------------------------------SARAIVA--------------------------------- */

            // ENTRANDO NO SITE MEGA LEITORES
            chrome.Navigate().GoToUrl("https://www.saraiva.com.br/");

            // LOCALIZANDO O LIVRO           
            chrome.FindElement(By.Name(@"?q=")).SendKeys(strISBN);
            chrome.FindElement(By.Name(@"?q=")).SendKeys(Keys.Enter);

            // CLICANDO NO LIVRO PESQUISADO
            IWebElement eleSaraLink = chrome.FindElement(By.CssSelector("a[class='nm-product-img-link']"));
            eleSaraLink.Click();

            // BUSCANDO AUTOR E ISBN
            IWebElement eleSara = chrome.FindElement(By.CssSelector("table[class='table mb-0']"));

            // PEGANDO DADOS DO AUTOR E ISBN

            var colSara = eleSara.FindElements(By.CssSelector("td[class='pl-0']"));

            isAutor = false;
            foreach (var td in colSara)
            {
                if (isAutor == true)
                {
                    strAutorSara = td.Text;
                    break;
                }

                if (td.Text.Trim() == "Autor")
                {
                    isAutor = true;
                }
            }

            // VERIFICANDO SE AUTOR E DIFERENTE
            if (strAutor != strAutorSara)
            {
                Assert.Fail("O autor do livro da Saraiva é diferente do autor do livro no submarino");
            }

            /* ---------------------------------SARAIVA--------------------------------- */
        }
    }
}
