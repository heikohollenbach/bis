cd ..
cd backend

docker build --build-arg PROJECT=WebService -t bis-web-service:1.0.0 .

cd ..
cd scripts
