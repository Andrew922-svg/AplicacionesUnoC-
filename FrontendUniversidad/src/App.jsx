import { BrowserRouter, Route, Routes } from "react-router-dom"
import { Listar } from "./Componets/estudiante"
import { ListarMaterias } from "./Componets/materias"
import { ListarNotas } from "./Componets/notas"

function App() {
  return(
    <BrowserRouter>
      <Routes>
        <Route path="/"element = {<h1><center>Biemvenido</center></h1>} />
        <Route path="/api/Usuario/listar/estudiante" element = {<Listar></Listar>}/>
        <Route path="/api/Usuario/listar/materias" element = {<ListarMaterias></ListarMaterias>}/>
        <Route path="/api/Usuario/listar/notas" element = {<ListarNotas></ListarNotas>} />
      </Routes>
    </BrowserRouter>
  )
}



export default App
