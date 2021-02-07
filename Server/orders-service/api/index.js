const router = require('express').Router();
const models = require('../models');
const auth = require('../modules/auth');

router.get('/orders', auth('Admin'), (req, res, next) => {
  let startDate = new Date(1970, 1, 1).toISOString();
  let endDate = new Date(2050, 1, 1).toISOString();
  
  if (req.query.startDate) {
    startDate = new Date(req.query.startDate).toISOString();
  }
  
  if (req.query.endDate) {
    endDate = new Date(req.query.endDate).toISOString();
  }

  models.Order.find({
    timeStamp: {
      $gte: startDate,
      $lte: endDate
    }
  })
  .sort({ timeStamp: 'desc' })
  .limit(20)
  .then(orders => res.send(orders))
  .catch(next);
})

module.exports = router;
