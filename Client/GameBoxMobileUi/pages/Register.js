import React from 'react';
import { StyleSheet, View, Text } from 'react-native';

const Register = () => {
  return (
    <View style={styles.page}>
      <View style={styles.content}>
        <Text style={styles.mainText}>Register Page!</Text>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  page: {
    flex: 1,
  },
  content: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: 'white',
  },
  mainText: {
    color: 'black',
    fontSize: 18,
    fontWeight: 'bold',
  },
});

export default Register;
