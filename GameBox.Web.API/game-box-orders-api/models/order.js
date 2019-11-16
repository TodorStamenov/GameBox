const mongoose = require('mongoose');

const orderSchema = new mongoose.Schema({
  username: {
    type: String,
    required: true
  },
  timeStamp: {
    type: String,
    required: true
  },
  price: {
    type: Number,
    required: true
  },
  gamesCount: {
    type: Number,
    required: true
  }
});

module.exports = mongoose.model('Order', orderSchema);
