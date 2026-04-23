cd ..
cd backend

docker build --build-arg PROJECT=PlayerService -t bis-player-service:1.0.0 .

cd ..
cd scripts
