
import { BrowserRouter, Route } from 'react-router'
import { Routes } from 'react-router'
import Layout from './pages/Layout'
import Home from './Pages/Home'
import Student from './Pages/Student'
import Teacher from './Pages/Teacher'
import Grade from './Pages/Grade'
import './App.css'

function App() {

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route index element={<Home />} />
          <Route path="student" element={<Student />} />
          <Route path="teacher" element={<Teacher />} />
          <Route path="grade" element={<Grade />} />
        </Route>
      </Routes>
    </BrowserRouter>
  )
}

export default App
