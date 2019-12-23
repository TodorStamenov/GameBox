FROM node:12.14.0
EXPOSE 1337

WORKDIR /app

COPY GameBox.Web.API/game-box-orders-api/package.json .
COPY GameBox.Web.API/game-box-orders-api/package-lock.json .

RUN npm install

COPY GameBox.Web.API/game-box-orders-api .

CMD ["node", "index.js"]