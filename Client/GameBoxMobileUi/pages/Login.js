import React from 'react';
import { StyleSheet, View, Text, TouchableNativeFeedback } from 'react-native';

const Login = ({ navigation }) => {
  return (
    <View style={styles.page}>
      <View style={styles.content}>
        <TouchableNativeFeedback
          onPress={() => navigation.navigate('Register')}>
          <Text style={styles.mainText}>Login Page!</Text>
        </TouchableNativeFeedback>
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

export default Login;
