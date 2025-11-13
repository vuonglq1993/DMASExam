import React, { useState } from "react";
import axios from "axios";

export default function CreateAsset() {
    const [assetName, setAssetName] = useState("");
    const [assetType, setAssetType] = useState("");

    const submit = async () => {
        try {
            const res = await axios.post("http://localhost:7071/api/createasset", {
                AssetName: assetName,
                AssetType: assetType
            });
            alert(res.data.message);
            setAssetName(""); setAssetType("");
        } catch (e) {
            alert("Error: " + (e.response?.data || e.message));
        }
    };

    return (
        <div>
            <input placeholder="Asset Name" value={assetName} onChange={e=>setAssetName(e.target.value)} />
            <input placeholder="Asset Type" value={assetType} onChange={e=>setAssetType(e.target.value)} />
            <button onClick={submit}>Create Asset</button>
        </div>
    );
}
