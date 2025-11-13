import React from "react";
import AssetsByPlayer from "./components/AssetsByPlayer";
import CreatePlayer from "./components/CreatePlayer";
import CreateAsset from "./components/CreateAsset";

function App() {
  return (
      <div style={{ padding: 20 }}>
        <h2>Battle Game Admin / Player Interface</h2>
        <hr />
        <h3>1. Create Player</h3>
        <CreatePlayer />
        <hr />
        <h3>2. Create Asset</h3>
        <CreateAsset />
        <hr />
        <h3>3. Assets By Player</h3>
        <AssetsByPlayer />
      </div>
  );
}

export default App;
