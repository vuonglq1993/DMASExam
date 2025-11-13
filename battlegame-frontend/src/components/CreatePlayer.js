import React, { useState } from "react";
import axios from "axios";

export default function CreatePlayer() {
    const [playerName, setPlayerName] = useState("");
    const [fullName, setFullName] = useState("");
    const [age, setAge] = useState("");
    const [level, setLevel] = useState("");

    const submit = async () => {
        try {
            const res = await axios.post("http://localhost:7071/api/registerplayer", {
                PlayerName: playerName,
                FullName: fullName,
                Age: parseInt(age),
                CurrentLevel: parseInt(level)
            });
            alert(res.data.message);
            setPlayerName(""); setFullName(""); setAge(""); setLevel("");
        } catch (e) {
            alert("Error: " + (e.response?.data || e.message));
        }
    };

    return (
        <div>
            <input placeholder="Player Name" value={playerName} onChange={e=>setPlayerName(e.target.value)} />
            <input placeholder="Full Name" value={fullName} onChange={e=>setFullName(e.target.value)} />
            <input placeholder="Age" type="number" value={age} onChange={e=>setAge(e.target.value)} />
            <input placeholder="Level" type="number" value={level} onChange={e=>setLevel(e.target.value)} />
            <button onClick={submit}>Create Player</button>
        </div>
    );
}
