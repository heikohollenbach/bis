cd ..
cd backend

docker build --build-arg PROJECT=ChatService -t bis-chat-service:1.0.0 .

cd ..
cd scripts
