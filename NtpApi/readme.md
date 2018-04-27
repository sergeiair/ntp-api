
* build: docker build -t sergeizh/ntp -f ./NtpApi/Dockerfile .
* commit: docker push sergeizh/ntp:latest
* pull: docker pull sergeizh/ntp:latest

* rm old one: docker rm ntp
* run: docker run -d -p 8080:80 --name ntp sergeizh/ntp
