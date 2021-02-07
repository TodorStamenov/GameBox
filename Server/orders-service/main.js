const express = require('express');
const cors = require('cors');
const config = require('./config/config');
const apiRouter = require('./api');
const app = express();

app.use(cors());
app.use(express.json());
app.use('/api', apiRouter);

app.use((err, req, res) => {
  console.error(err);
  res.status(500).send('Server Error');
});

app.listen(config.nodePort, () => {
  console.log(`Server listening on port ${config.nodePort}`);
  require('./modules/mq');
});
