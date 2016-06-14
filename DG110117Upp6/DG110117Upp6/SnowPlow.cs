using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DG110117Upp6
{
    public class SnowPlow
    {
        private Game game;
        private Matrix objectWorld = Matrix.CreateScale(0.1f) * Matrix.CreateTranslation(new Vector3(4f, 0.2f, -1));
        private Model snowPlowModel;

        Matrix[] boneTransforms;

        private Effect uppA;
        private Effect uppB;

        #region ModelBones
        // this will be the bones to rotate
        private ModelBone rwheel2R;
        private ModelBone rwheel2L;
        private ModelBone rwheelR;
        private ModelBone rwheelL;

        private ModelBone plow;
        #endregion

        #region ModelBoneTransforms
        // this will be the the original transforms for the bones
        private Matrix rwheel2RTransform;
        private Matrix rwheel2LTransform;
        private Matrix rwheelRTransform;
        private Matrix rwheelLTransform;

        private Matrix plowTransform;
        #endregion

        #region Rotations
        // this will be the bones rotationalvalue
        // private float wheelRotation;
        private float plowRotation = 0.0f;
        #endregion


        #region Properties

        public float PlowRotation
        {
            get
            {
                return plowRotation;
            }
            set
            {
                plowRotation = value;
            }
        }
        #endregion
        public SnowPlow(Game game)
        {
            this.game = game;
            this.snowPlowModel = game.Content.Load<Model>("Models/snowplow");
            this.uppA = game.Content.Load<Effect>("UppA");
            this.uppB = game.Content.Load<Effect>("UppB");
            boneTransforms = new Matrix[snowPlowModel.Bones.Count];
            rwheel2R = snowPlowModel.Bones["rwheel2.R"];
            rwheel2L = snowPlowModel.Bones["rwheel2.L"];
            rwheelR = snowPlowModel.Bones["rwheel.R"];
            rwheelL = snowPlowModel.Bones["rwheel.L"];
            plow = snowPlowModel.Bones["Cube.017"];

            rwheel2RTransform = rwheel2R.Transform;
            rwheel2LTransform = rwheel2L.Transform;
            rwheelRTransform = rwheelR.Transform;
            rwheelLTransform = rwheelL.Transform;
            plowTransform = plow.Transform;

            //rwheel2R.Transform = Matrix.CreateScale(3f) * Matrix.CreateTranslation(0, 3f, 0) * rwheel2RTransform;
            //rwheelR.Transform =  Matrix.CreateScale(3f) * Matrix.CreateTranslation(0, 0f, 0) * rwheelRTransform;
        }


        public void Update(GameTime gameTime)
        {
            objectWorld *= Matrix.CreateTranslation(-0.001f, 0, 0);
        }

        public void Draw(BasicEffect basicEffect)
        {
            snowPlowModel.Root.Transform = objectWorld;// Matrix.CreateScale(0.05f) * Matrix.CreateTranslation(new Vector3(0.3f, 0.2f, -1f));

            plow.Transform = Matrix.CreateRotationY(plowRotation) * plowTransform;

            snowPlowModel.CopyAbsoluteBoneTransformsTo(boneTransforms);
            foreach (ModelMesh mesh in snowPlowModel.Meshes)
            {
                if (mesh.Name == "Cube.012")
                {
                    foreach (ModelMeshPart meshPart in mesh.MeshParts)
                    {
                        meshPart.Effect = uppA;
                        uppA.Parameters["World"].SetValue(boneTransforms[mesh.ParentBone.Index]);
                        uppA.Parameters["View"].SetValue(basicEffect.View);
                        uppA.Parameters["Projection"].SetValue(basicEffect.Projection);
                        uppA.CurrentTechnique.Passes[0].Apply();
                    }
                } else if(mesh.Name == "Cube.017" || mesh.Name == "Cube.013"){
                    foreach (ModelMeshPart meshPart in mesh.MeshParts)
                    {
                        meshPart.Effect = uppB;
                        uppB.Parameters["World"].SetValue(boneTransforms[mesh.ParentBone.Index]);
                        uppB.Parameters["View"].SetValue(basicEffect.View);
                        uppB.Parameters["Projection"].SetValue(basicEffect.Projection);
                        uppB.CurrentTechnique.Passes[0].Apply();
                    }
                }
                else
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.World = boneTransforms[mesh.ParentBone.Index];
                        effect.View = basicEffect.View;
                        effect.Projection = basicEffect.Projection;
                        // effect.CurrentTechnique.Passes[0].Apply();
                    }
                }
                mesh.Draw();
            }
        }

        public void Draw(Matrix world, Matrix view, Matrix projection, BasicEffect basicEffect)
        {

            Draw(basicEffect);
        }

    }
}
