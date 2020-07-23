const amqp = require('amqp');
const config = require('./config');

module.exports = amqp.createConnection({
  host: config.dockerHostIp,
  port: config.rabbitMqPort,
  login: 'guest',
  password: 'guest'
});
