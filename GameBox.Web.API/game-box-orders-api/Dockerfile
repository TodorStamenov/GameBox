FROM node:12.18.2-alpine
EXPOSE 1337

WORKDIR /app

COPY GameBox.Web.API/game-box-orders-api/package.json .
COPY GameBox.Web.API/game-box-orders-api/package-lock.json .

RUN npm install

COPY GameBox.Web.API/game-box-orders-api .

ENTRYPOINT ["node", "index.js"]