echo "Restoring dotnet configuration"
cd adventofcode
dotnet restore

echo "Setting up tabnine_config"
mkdir -p $HOME/.config/TabNine
touch $HOME/.config/TabNine/tabnine_config.json