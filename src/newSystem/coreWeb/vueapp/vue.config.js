module.exports = {
    devServer: {
      proxy: {
        '^/api': {
          target: 'http://localhost:5000/'
        },
        '^/foo': {
          target: '<other_url>'
        }
      }
    }
  }