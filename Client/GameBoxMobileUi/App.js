import React from 'react';
import { StyleSheet, View, Text } from 'react-native';

const App = () => {
  return (
    <View style={styles.home}>
      <Text style={styles.mainText}>Hello world!</Text>
    </View>
  );
};

const styles = StyleSheet.create({
  home: {
    flex: 1,
    alignItems: 'center',
    backgroundColor: 'white',
    justifyContent: 'center',
  },
  mainText: {
    color: 'black',
    fontSize: 18,
    fontWeight: 'bold',
  },
});

export default App;
