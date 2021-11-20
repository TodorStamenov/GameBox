import React, { useState } from 'react';
import { StatusBar } from 'expo-status-bar';
import { FlatList, StyleSheet, Text, View } from 'react-native';

export default App = () => {
  const [todos, setTodos] = useState([
    { key: '1', text: 'eat' },
    { key: '2', text: 'code' },
    { key: '3', text: 'sleep' },
    { key: '4', text: 'repeat' }
  ]);

  return (
    <View style={styles.container}>
      <View style={styles.content}>
        <FlatList
          data={todos}
          renderItem={({ item }) => (
            <Text>{item.text}</Text>
          )}
        />
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    alignItems: 'center',
    justifyContent: 'center',
    padding: 40,
  },
  content: {
    marginTop: 20,
  },
});
