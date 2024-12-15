
import { BrowserRouter, Route } from 'react-router'
import { Routes } from 'react-router'
import Layout from './Pages/Layout'
import Home from './Pages/Home'
import Student from './Pages/Students/Student'
import Teacher from './Pages/Teachers/Teacher'
import Grade from './Pages/Grades/Grade'
import './App.css'
import 'react-toastify/dist/ReactToastify.css';
import { PrimeReactProvider } from 'primereact/api';
import "primereact/resources/themes/lara-light-cyan/theme.css";
import StudentCU from './Pages/Students/StudentCU'
import TeacherCU from './Pages/Teachers/TeacherCU'
import GradeCU from './Pages/Grades/GradeCU'
import GradeStudentCU from './Pages/Grades/GradeStudentCU'


function App() {

  return (
    <PrimeReactProvider>
      <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route index element={<Home />} />
          <Route path="student" element={<Student />} />
          <Route path="student/:id" element={<StudentCU />} />
          <Route path="teacher" element={<Teacher />} />
          <Route path="teacher/:id" element={<TeacherCU />} />
          <Route path="grade" element={<Grade />} />
          <Route path="grade/:id" element={<GradeCU />} />
          <Route path="grade/:idGrade/:idStudent" element={<GradeStudentCU />} />
        </Route>
      </Routes>
    </BrowserRouter>
    </PrimeReactProvider>
    
  )
}

export default App
