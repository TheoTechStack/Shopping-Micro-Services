 docker build -t shopping-micro-services .
 docker run -p 8080:80 shopping-micro-services   
 docker-compose -f docker-compose.debug.yml up --build
 Default: http://localhost:8080
 
 ----------------------------------------------------
 Rebuild the Docker image and restart the container:
 ----------------------------------------------------
docker-compose down
docker-compose build
docker-compose up