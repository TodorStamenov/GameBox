FROM node:16.18-alpine
EXPOSE 1337

WORKDIR /app

COPY orders-service/package.json orders-service/package-lock.json ./
RUN npm install

COPY orders-service .
RUN npm run build

ENTRYPOINT ["node", "dist/main"]