cd ..
cd backend

docker build --build-arg PROJECT=WorldService -t bis-world-service:1.0.0 .

cd ..
cd scripts
