const amqp = require('amqp');

module.exports = amqp.createConnection({
  host: '192.168.99.100',
  port: 5672,
  login: 'guest',
  password: 'guest'
});
