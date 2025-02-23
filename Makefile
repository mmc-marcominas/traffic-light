ignore := 'test*|obj|bin|Release|Debug'
basePath := ~/projects/personal/interviews/swile
bin := ./bin/Release/net7.0/TrafficLight

# Usage samples:
# 
#   make build 
#   make publish
#   make test
#   make run 

build:
		@dotnet build TrafficLight.csproj --configuration Release

publish:
		@dotnet publish TrafficLight.csproj --configuration Release

run:
		@$(bin)

clean:
		@cd $(basePath) && rm -rf ./bin ./obj

tree:
		@tree . -I $(ignore)

all: clean build publish tree run
