using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsdrubalApp {
    public partial class MainPage : ContentPage {

        private List<int> pressed_keys = new List<int>();

        public MainPage() {
            InitializeComponent();

            ConButton.Clicked += ConButton_Clicked;
        }

        private async void ConButton_Clicked(object sender, EventArgs e) {
            var client = new TcpClient();
            await client.ConnectAsync("10.0.0.102", 1209);
            ConButton.StyleClass.Clear();
            ConButton.StyleClass.Add("Succes");
            ConButton.IsEnabled = false;
            await Task.Run(() => {
                while (true) {
                    var s = "";
                    foreach (int i in pressed_keys) {
                        s += i;
                    }
                    client.Client.Send(Encoding.UTF8.GetBytes(s));
                    byte[] b = new byte[256];
                    client.Client.Receive(b);
                }
            });
        }
        // 0 = R, 1 = D, 2 = L, 3 = U
        private void RClick() {
            if (!pressed_keys.Contains(0)) pressed_keys.Add(0);
        }
        private void ROut() {
            pressed_keys.Remove(3);
            pressed_keys.Add(4);
        }
        private void DClick() {
            if (!pressed_keys.Contains(1)) pressed_keys.Add(1);
        }
        private void DOut() {
            pressed_keys.Remove(1);
            pressed_keys.Add(5);
        }
        private void LClick() {
            if (!pressed_keys.Contains(2)) pressed_keys.Add(2);
        }
        private void LOut() {
            pressed_keys.Remove(2);
            pressed_keys.Add(6);
        }
        private void UClick() {
            if (!pressed_keys.Contains(3)) pressed_keys.Add(3);
        }
        private void UOut() {
            pressed_keys.Remove(3);
            pressed_keys.Add(7);
        }
    }
}
