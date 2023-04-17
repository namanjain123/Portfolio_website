import { Col } from "react-bootstrap";
import Fade from "react-awesome-reveal";
import React from 'react';
export const ProjectCard = ({ title, description, imgUrl }) => {
  return (
    <Col size={12} sm={6} md={4}>
      <Fade bottom duration={1000} distance="20px">
      <div className="proj-imgbx">
        <img src={imgUrl} />
        <div className="proj-txtx">
          <h4>{title}</h4>
          <span>{description}</span>
        </div>
      </div>
      </Fade>
    </Col>
  )
}