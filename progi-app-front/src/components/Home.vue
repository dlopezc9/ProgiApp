<template>
<div class="table-container">
    <h2>Vehicles</h2>
    <table>
        <thead>
            <tr>
                <th v-for="column in columns" :key="column">{{ column }}</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="(row, index) in list" :key="index">
                <td>{{ row.vehiclePrice }}</td>
                <td>{{ row.vehicleType }}</td>
                <td>{{ row.basicFee }}</td>
                <td>{{ row.specialFee }}</td>
                <td>{{ row.associationFee }}</td>
                <td>{{ row.storageFee }}</td>
                <td>{{ row.total }}</td>
            </tr>
        </tbody>
    </table>
</div>
<button v-on:click="mounted()">Load table</button>
</template>

<script>
import axios from "axios"
export default {
    name: "HomeTest",
    

    data() {
        return {
            list: [],
            columns: ["Vehicle Price ($)", "Vehicle Type", "Basic", "Special", "Association", "Storage", "Total($)"]
        }
    },
    methods: {
        async mounted() {
            let result = await axios.get("https://localhost:44314/Calculations")
            this.list = result.data
        }

    }

}
</script>

<style scoped>
    body {
      font-family: Arial, sans-serif;
      margin: 20px;
    }
    .table-container {
      margin: 20px 0;
      border: 1px solid #ccc;
      border-radius: 5px;
      overflow: hidden;
    }
    table {
      width: 100%;
      border-collapse: collapse;
    }
    thead {
      background-color: #333;
      color: #fff;
    }
    th, td {
      padding: 10px;
      text-align: center;
      border-bottom: 1px solid #ddd;
    }
    tr:hover {
      background-color: #f5f5f5;
    }
  </style>