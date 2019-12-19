FROM node:12.14.0
EXPOSE 1337

WORKDIR /app

COPY GameBox.Web.API/game-box-orders-api .

RUN npm install

CMD ["node", "index.js"]