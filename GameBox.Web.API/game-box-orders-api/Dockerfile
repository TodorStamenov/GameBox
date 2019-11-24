FROM node:10
EXPOSE 1337

WORKDIR /app

COPY GameBox.Web.API/game-box-orders-api .

RUN npm install

CMD ["node", "index.js"]