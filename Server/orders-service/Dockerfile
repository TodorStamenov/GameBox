FROM node:14.15.5-alpine
EXPOSE 1337

WORKDIR /app

COPY orders-service/package.json .
COPY orders-service/package-lock.json .

RUN npm install

COPY orders-service .

ENTRYPOINT ["node", "index.js"]