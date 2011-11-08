﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace MetaCase.GraphBrowser
{
    class GraphViewModel
    {
        private Graph graph;
		private GraphViewModel parent;
        public String Name
        {
            get { return this.ToString(); }
        }
		private readonly ICollection<GraphViewModel> _children = new ObservableCollection<GraphViewModel>();
        public ICollection<GraphViewModel> children
        {
            get { return _children; }
        }
		
		public GraphViewModel(Graph _graph)
        {
			this.graph = _graph;
			this._children = new List<GraphViewModel>();
		}

		public GraphViewModel()
        {
			this.graph = null;
			this._children = new List<GraphViewModel>();
		}

		public void setParent(GraphViewModel parent)
        {
			this.parent = parent;
		}

		public GraphViewModel getParent()
        {
			return parent;
		}

        public override String ToString()
        {
            return this.graph.Name;
        }

		public Graph getGraph()
        {
			return this.graph;
		}

        public void addChild(GraphViewModel child)
        {
			_children.Add(child);
			child.setParent(this);
		}

		public void removeChild(GraphViewModel child)
        {
			_children.Remove(child);
			child.setParent(null);
		}

		public GraphViewModel [] getChildren()
        {
            return _children.ToArray();
		}

		public Boolean hasChildren() {
			return _children.Count > 0;
		}
		
		public void populate(Graph[] graphs, List<Graph> stack)
        {
			if (!stack.Contains(this.getGraph())) {
				stack.Add(this.getGraph());
				foreach (Graph g in graphs) {
					GraphViewModel gvm = new GraphViewModel(g);
					this.addChild(gvm);
					if (g.GetChildren() != null && g.GetChildren().Count() > 0) {
						gvm.populate(g.GetChildren(), stack);
					}
				}
			}
		}
    }
}
