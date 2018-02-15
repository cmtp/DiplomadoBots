using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace HelloBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            // return our reply to the user
            //await context.PostAsync($"You sent {activity.Text} which was {length} characters");
            await context.PostAsync(ChatBot(activity.Text.ToUpper()));
            context.Wait(MessageReceivedAsync);
        }

        private string ChatBot(string text) {
            string result = "";

            if (text.Contains("HOLA"))
            {
                return "Un saludo. En que podemos colaborarle?.";
            }
            else if (text.Contains("INSCRI")) {
                result = "El proceso de inscripcion es muy sencillo, por favor dirijase al siguiente link: https://ticapacitacion.com/curso/botses/";
            }
            else if (text.Contains("PROBLEMAS CON")) {
                result = "En este momento estamos realizando algunos ajustes, por favor intente mas tarde";
            }
            else if (text.Contains("DESCARGAR VIDEOS")) {
                result = "Por favor comuniquese con los administradores del curso";
            }
            else if (text.Contains("DESCARGAR CONTENIDO")) {
                result = "Algunos de los modulos tienen contenido para descargar, pero no todos, aparece un icono correspondiente a la descarga del contenido en PDF en el video que tenga esa informacion";
            }
            else if (text.Contains("NO GRACIAS")) {
                result = "Esperamos haber sido de ayuda, que este muy bien";
            }
            else {
                result = "Ese es un tema del que no tenemos informacion en este momento";
            }

            result += "\nPodemos colaborarle con algo mas?";

            return result;
        }
    }
}