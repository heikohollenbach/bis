cd ..
cd backend

docker build --build-arg PROJECT=AuthService -t bis-auth-service:1.0.0 .

cd ..
cd scripts
