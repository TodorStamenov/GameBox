const connection = require('../config/mq');
const models = require('../models');

connection.on('ready', function () {
  console.log('Connected to RabbitMQ');

  connection.queue('orders', function (q) {
    q.bind('#');

    q.subscribe(function (message) {
      const { username, price, gamesCount, timeStamp } = JSON.parse(message.data.toString('utf8'));

      models.Order
        .create({ username, price, gamesCount, timeStamp })
        .then(() => q.shift());
    });
  });
});
