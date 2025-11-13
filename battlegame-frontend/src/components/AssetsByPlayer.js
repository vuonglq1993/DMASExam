import React, { useState } from "react";
import axios from "axios";

export default function AssetsByPlayer() {
    const [playerId, setPlayerId] = useState("");
    const [rows, setRows] = useState([]);

    const load = async () => {
        try {
            const res = await axios.get(`http://localhost:7071/api/getassetsbyplayer/${playerId}`);
            setRows(res.data);
        } catch (e) {
            alert("Error: " + (e.response?.data || e.message));
        }
    };

    return (
        <div>
            <input
                placeholder="Enter Player ID (GUID)"
                value={playerId}
                onChange={e=>setPlayerId(e.target.value)}
                style={{width:400}}
            />
            <button onClick={load}>Load Assets</button>

            <table border="1" cellPadding="6" style={{marginTop:10}}>
                <thead>
                <tr>
                    <th>No</th>
                    <th>Player Name</th>
                    <th>Level</th>
                    <th>Age</th>
                    <th>Asset Name</th>
                    <th>Quantity</th>
                </tr>
                </thead>
                <tbody>
                {rows.length===0
                    ? <tr><td colSpan="6">No data</td></tr>
                    : rows.map((r,i)=>(
                        <tr key={i}>
                            <td>{i+1}</td>
                            <td>{r.PlayerName}</td>
                            <td>{r.Level}</td>
                            <td>{r.Age}</td>
                            <td>{r.AssetName}</td>
                            <td>{r.Quantity || 1}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}
