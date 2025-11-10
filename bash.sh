# 1. Atualiza os pacotes
sudo apt update

# 2. Instala dependências necessárias
sudo apt install -y wget apt-transport-https ca-certificates

# 3. Baixa o pacote oficial do Chrome
wget https://dl.google.com/linux/direct/google-chrome-stable_current_amd64.deb

# 4. Instala o pacote
sudo dpkg -i google-chrome-stable_current_amd64.deb

# 5. Corrige dependências (caso apareça erro de libs)
sudo apt -f install -y

# 6. Remove o arquivo .deb pra limpar
rm google-chrome-stable_current_amd64.deb

# 7. Verifica a instalação
google-chrome --version